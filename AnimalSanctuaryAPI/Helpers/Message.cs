namespace AnimalSanctuaryAPI.Helpers
{
    public static class Message
    {
        public const string ERROR = "Error {msg}";
        public const string MSG_NORECORDS = "NO RECORDS FOUND.";
        public const string MSG_NOTFOUND = "ID: {id} NOT FOUND.";
        public const string MSG_CREATED = "ID: {id} CREATED.";
        public const string MSG_DELETED = "ID: {id} DELETED.";
        public const string MSG_UPDATED = "ID: {id} UPDATED.";
        public const string MSG_SAVED = "ID: {id} saved to database.";
        public const string MSG_NAMEINUSE = "Name already in use";
    }
}