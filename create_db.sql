create table SavedData(
    Id          int              identity,
    Guid        uniqueidentifier not null,
    Message     nvarchar(250)    not null,
    IsLink      bit              not null,
    [User]      uniqueidentifier     null,
    Created     datetime         not null default(getdate()),
    TimeLimit   int                  null,
    EndTime     as (Created+TimeLimit),
    ClickLimit  int                  null,
    [Group]     uniqueidentifier     null,

    constraint SavedData_Id_pk primary key clustered (Id),
)
create unique nonclustered index SavedData_Guid_index on SavedData(Guid)

create table ShortCodes(
    ShortCode   int              identity,
    RelatedGuid uniqueidentifier not null,

    constraint ShortCodes_ShortCode_pk primary key clustered (ShortCode),
    constraint ShortCodes_RelatedGuid_fk foreign key (RelatedGuid) references SavedData(Guid) on delete cascade on update cascade,
)
create unique nonclustered index ShortCodes_RelatedGuid_index on ShortCodes(RelatedGuid)

go
