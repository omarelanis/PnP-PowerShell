﻿using OfficeDevPnP.Core.Framework.Graph;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;
using SharePointPnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SharePointPnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsChannel")]
    [CmdletHelp("Gets the channels for a specified Team.",
       Category = CmdletHelpCategory.Teams,
       SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
      Code = "PS:> Get-PnPTeamsChannel -GroupId a6c1e0d7-f579-4993-81ab-4b666f8edea8",
      Remarks = "Retrieves all channels for the specified team",
      SortOrder = 1)]
    [CmdletExample(
      Code = "PS:> Get-PnPTeamsChannel -GroupId a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Identity \"Test Channel\"",
      Remarks = "Retrieves the channel called 'Test Channel'",
      SortOrder = 2)]
    [CmdletExample(
      Code = "PS:> Get-PnPTeamsChannel -GroupId a6c1e0d7-f579-4993-81ab-4b666f8edea8 -Identity \"19:796d063b63e34497aeaf092c8fb9b44e@thread.skype\"",
      Remarks = "Retrieves the channel specified by its channel id",
      SortOrder = 3)]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_Read_All)]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Group_ReadWrite_All)]
    public class GetTeamsChannel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public GuidPipeBind GroupId;

        [Parameter(Mandatory = false)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var channels = TeamsUtility.GetChannels(AccessToken, HttpClient, GroupId.Id.ToString());
                WriteObject(channels.FirstOrDefault(c => c.DisplayName.Equals(Identity, StringComparison.OrdinalIgnoreCase) || c.Id.Equals(Identity, StringComparison.OrdinalIgnoreCase)));
            }
            else
            {
                WriteObject(TeamsUtility.GetChannels(AccessToken, HttpClient, GroupId.Id.ToString()));
            }
        }
    }
}