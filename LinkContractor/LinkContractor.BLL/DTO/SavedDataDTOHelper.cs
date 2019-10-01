using System;
using LinkContractor.BLL.Message;
using LinkContractor.DAL.Models;

namespace LinkContractor.BLL.DTO
{
    internal static class SavedDataDTOHelper
    {
        public static SavedDataDTO ToDTO(this SavedData savedData)
        {
            var savedDataDTO = new SavedDataDTO
            {
                Guid = savedData.Guid,
                Message = savedData.Message,
                IsLink = savedData.IsLink,
                User = savedData.User,
                Created = savedData.Created,
                TimeLimit = savedData.TimeLimit,
                EndTime = savedData.EndTime,
                ClickLimit = savedData.ClickLimit,
                Group = savedData.Group,
                Code = savedData.ShortCode?.Code
            };
            return savedDataDTO;
        }

        public static SavedData ToSavedData(this SavedDataDTO savedDataDTO)
        {
            var savedData = new SavedData
            {
                Guid = savedDataDTO.Guid,
                Message = savedDataDTO.Message,
                IsLink = savedDataDTO.IsLink,
                User = savedDataDTO.User,
                Created = savedDataDTO.Created,
                TimeLimit = savedDataDTO.TimeLimit,
                EndTime = savedDataDTO.EndTime,
                ClickLimit = savedDataDTO.ClickLimit,
                Group = savedDataDTO.Group
            };
            return savedData;
        }

        public static ShortCode ToShortCode(this SavedDataDTO savedDataDTO)
        {
            var shortCode = new ShortCode
            {
                RelatedGuid = savedDataDTO.Guid
            };
            return shortCode;
        }

        public static bool IsRecordNotExpired(this SavedData savedData)
        {
            return savedData.EndTime is null || savedData.EndTime > DateTime.UtcNow;
        }

        public static bool DecrementClick(this SavedData savedData)
        {
            if (savedData.ClickLimit is null) return true;
            if (savedData.ClickLimit <= 0) return false;

            savedData.ClickLimit--;
            return true;
        }

        public static IMessage GetMessage(this SavedData savedData)
        {
            return LinkMessage.TryCreateLinkMessageIfNeeded(savedData.Message, savedData.IsLink);
        }

        public static void SwitchContent(SavedDataDTO first, SavedData second)
        {
            (first.Message, second.Message) = (second.Message, first.Message);
            (first.IsLink, second.IsLink) = (second.IsLink, first.IsLink);
            (first.ClickLimit, second.ClickLimit) = (second.ClickLimit, first.ClickLimit);
            (first.TimeLimit, second.TimeLimit) = (second.TimeLimit, first.TimeLimit);
        }

        public static void SendToHistory(this SavedDataDTO savedDataDTO)
        {
            savedDataDTO.TimeLimit = null;
            savedDataDTO.ClickLimit = -1;
        }
    }
}