using System;
using LinkContractor.BLL.DTO;
using LinkContractor.BLL.Message;
using LinkContractor.DAL;
using LinkContractor.DAL.Models;

namespace LinkContractor.BLL
{
    public class BlMain
    {
        public BlMain(IUnitOfWork unitOfWorkParam)
        {
            UnitOfWork = unitOfWorkParam ?? throw new ArgumentNullException(nameof(unitOfWorkParam));
        }

        private IUnitOfWork UnitOfWork { get; }

        public string AddRecord(SavedDataDTO savedDataDTO, bool isShortLinkNeeded = false)
        {
            if (savedDataDTO == null) throw new ArgumentNullException(nameof(savedDataDTO));

            string result;
            UnitOfWork.SavedData.Add(savedDataDTO.ToSavedData());
            if (isShortLinkNeeded)
            {
                var sc = savedDataDTO.ToShortCode();
                UnitOfWork.ShortCodes.Add(sc);
                Save();
                result = sc.Code.Convert();
            }
            else
            {
                Save();
                result = savedDataDTO.Guid.Convert();
            }

            return result;
        }

        private SavedData Find(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            var isShort = code.IsShortCode();
            SavedData result;
            if (isShort)
            {
                var i = code.ConvertToInt();
                if (i is null) return null;
                var id = i.Value;
                result = UnitOfWork.ShortCodes.GetCorrespondingSavedData(id);
            }
            else
            {
                var gi = code.ConvertToGuid();
                if (gi is null) return null;
                var guidCode = gi.Value;
                result = UnitOfWork.SavedData.GetSavedDataWithShortCode(guidCode);
            }

            return result;
        }

        public bool ChangeRecord(string code, SavedDataDTO savedDataDTO)
        {
            if (savedDataDTO.User == Guid.Empty) return false;

            var first = Find(code);
            if (first is null) return false;

            if (first.User != savedDataDTO.User) return false;

            if (first.Group is null) first.Group = Guid.NewGuid();
            savedDataDTO.Group = first.Group;

            savedDataDTO.Guid = Guid.NewGuid();

            SavedDataDTOHelper.SwitchContent(savedDataDTO, first);
            savedDataDTO.SendToHistory();

            UnitOfWork.SavedData.Update(first);
            UnitOfWork.SavedData.Add(savedDataDTO.ToSavedData());

            Save();
            return true;
        }

        public IMessage Click(string code)
        {
            var record = Find(code);
            if (record is null) return null;

            if (record.IsRecordNotExpired() && record.DecrementClick())
            {
                Save();
                return record.GetMessage();
            }

            return null;
        }

        private void Save()
        {
            UnitOfWork.SaveChanges();
        }
    }
}