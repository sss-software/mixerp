﻿/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System.ComponentModel;
using System.IO;
using System.Web.Hosting;
using System.Web.Script.Services;
using System.Web.Services;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.FrontEnd.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class CreateDocument : WebService
    {
        [WebMethod]
        public void Create(string html, string documentName)
        {
            if (!this.Context.User.Identity.IsAuthenticated)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(html) || string.IsNullOrWhiteSpace(documentName))
            {
                return;
            }

            string destination = HostingEnvironment.MapPath("/Resource/Documents/" + documentName);

            if (File.Exists(destination))
            {
                return;
            }

            ExportHelper.CreatePDF(html, destination);

            System.Threading.Thread.Sleep(1000);
        }
    }
}