using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MediaPortal.Backend.Database;
using MediaPortal.Common;
using MediaPortal.Common.General;
using MediaPortal.Common.PathManager;

namespace MediaPortal.Extensions.MediaServer.Database
{
    class MediaServer_SubSchema
    {
        #region Consts

        public const string SUBSCHEMA_NAME = "MediaServer";

        public const int EXPECTED_SCHEMA_VERSION_MAJOR = 1;
        public const int EXPECTED_SCHEMA_VERSION_MINOR = 0;

        #endregion

        public static string SubSchemaScriptDirectory
        {
            get
            {
                var pathManager = ServiceRegistration.Get<IPathManager>();
                return pathManager.GetPath(@"<APPLICATION_ROOT>\Scripts\");
            }
        }

        public static IDbCommand SelectDlnaMediaInfoCommand(ITransaction transaction, out int systemIdIndex,
            out int lastHostNameIndex, out int lastClientNameIndex)
        {
            var result = transaction.CreateCommand();

            //result.CommandText = "SELECT SYSTEM_ID, LAST_HOSTNAME, LAST_CLIENT_NAME FROM DLNA_MEDIA_INFO";

            systemIdIndex = 0;
            lastHostNameIndex = 1;
            lastClientNameIndex = 2;
            return result;
        }

        public static IDbCommand InsertAttachedClientCommand(ITransaction transaction, string systemId, string hostName,
            string clientName)
        {
            var result = transaction.CreateCommand();
            result.CommandText = "INSERT INTO ATTACHED_CLIENTS (SYSTEM_ID, LAST_HOSTNAME, LAST_CLIENT_NAME) VALUES (@SYSTEM_ID, @LAST_HOSTNAME, @LAST_CLIENT_NAME)";
            ISQLDatabase database = transaction.Database;
            database.AddParameter(result, "SYSTEM_ID", systemId, typeof(string));
            database.AddParameter(result, "LAST_HOSTNAME", hostName, typeof(string));
            database.AddParameter(result, "LAST_CLIENT_NAME", clientName, typeof(string));
            return result;
        }

        public static IDbCommand UpdateAttachedClientDataCommand(ITransaction transaction, string systemId, SystemName system,
            string clientName)
        {
            var result = transaction.CreateCommand();
            result.CommandText = "UPDATE ATTACHED_CLIENTS SET LAST_HOSTNAME = @LAST_HOSTNAME, LAST_CLIENT_NAME = @LAST_CLIENT_NAME WHERE SYSTEM_ID = @SYSTEM_ID";
            ISQLDatabase database = transaction.Database;
            database.AddParameter(result, "LAST_HOSTNAME", system == null ? null : system.HostName, typeof(string));
            database.AddParameter(result, "LAST_CLIENT_NAME", clientName, typeof(string));
            database.AddParameter(result, "SYSTEM_ID", systemId, typeof(string));
            return result;
        }

        public static IDbCommand DeleteAttachedClientCommand(ITransaction transaction, string systemId)
        {
            var result = transaction.CreateCommand();
            result.CommandText = "DELETE FROM ATTACHED_CLIENTS WHERE SYSTEM_ID = @SYSTEM_ID";
            ISQLDatabase database = transaction.Database;
            database.AddParameter(result, "SYSTEM_ID", systemId, typeof(string));
            return result;
        }
    }
}
