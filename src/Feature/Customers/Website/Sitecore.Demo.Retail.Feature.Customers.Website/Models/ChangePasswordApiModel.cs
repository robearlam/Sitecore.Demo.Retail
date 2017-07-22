﻿//-----------------------------------------------------------------------
// <copyright file="ChangePasswordBaseJsonResult.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Emits the Json result of a change password.</summary>
//-----------------------------------------------------------------------
// Copyright 2016 Sitecore Corporation A/S
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file 
// except in compliance with the License. You may obtain a copy of the License at
//       http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the 
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
// either express or implied. See the License for the specific language governing permissions 
// and limitations under the License.
// -------------------------------------------------------------------------------------------

using Sitecore.Commerce.Services;
using Sitecore.Demo.Retail.Foundation.Commerce.Website.Models;
using Sitecore.Diagnostics;

namespace Sitecore.Demo.Retail.Feature.Customers.Website.Models
{
    public class ChangePasswordApiModel : BaseApiModel
    {
        public ChangePasswordApiModel()
        {
        }

        public ChangePasswordApiModel(ServiceProviderResult result) : base(result)
        {
        }

        public string UserName { get; set; }

        public void Initialize(string userName)
        {
            Assert.ArgumentNotNullOrEmpty(userName, nameof(userName));

            UserName = userName;
        }
    }
}