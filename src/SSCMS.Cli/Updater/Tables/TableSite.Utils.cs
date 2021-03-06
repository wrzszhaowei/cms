﻿using System.Collections.Generic;
using Datory;
using SSCMS.Models;
using SSCMS.Utils;

namespace SSCMS.Cli.Updater.Tables
{
    public partial class TableSite
    {
        public static readonly List<string> OldTableNames = new List<string>
        {
            "siteserver_PublishmentSystem",
            "wcm_PublishmentSystem"
        };

        public ConvertInfo Converter => new ConvertInfo
        {
            NewTableName = NewTableName,
            NewColumns = NewColumns,
            ConvertKeyDict = ConvertKeyDict,
            ConvertValueDict = ConvertValueDict,
            Process = Process
        };

        private string NewTableName => _databaseManager.SiteRepository.TableName;

        private List<TableColumn> NewColumns => _databaseManager.SiteRepository.TableColumns;

        private static readonly Dictionary<string, string> ConvertKeyDict =
            new Dictionary<string, string>
            {
                {nameof(Site.Id), nameof(PublishmentSystemId)},
                {nameof(Site.SiteName), nameof(PublishmentSystemName)},
                {nameof(Site.TableName), nameof(AuxiliaryTableForContent)},
                {nameof(Site.SiteDir), nameof(PublishmentSystemDir)},
                {"IsRoot", nameof(IsHeadquarters)},
                {nameof(Site.ParentId), nameof(ParentPublishmentSystemId)}
            };

        private static readonly Dictionary<string, string> ConvertValueDict = null;

        private static Dictionary<string, object> Process(Dictionary<string, object> row)
        {
            if (row.TryGetValue(nameof(IsHeadquarters), out var contentObj))
            {
                var value = TranslateUtils.ToBool(contentObj.ToString());
                row[nameof(Site.Root)] = value;
            }

            return row;
        }
    }
}
