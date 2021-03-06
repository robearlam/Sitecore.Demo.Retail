﻿//-----------------------------------------------------------------------
// <copyright file="AddressListItemJsonResult.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Defines the AddressListJsonResult class.</summary>
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

using System.Collections.Generic;
using System.Linq;
using Sitecore.Commerce.Services;
using Sitecore.Diagnostics;
using Sitecore.Foundation.Commerce.Website.Models;
using Sitecore.Mvc.Extensions;

namespace Sitecore.Feature.Commerce.Customers.Website.Models
{
    public class AddressListItemApiModel : BaseApiModel
    {
        public AddressListItemApiModel()
        {
        }

        public AddressListItemApiModel(ServiceProviderResult result)
            : base(result)
        {
        }


        public List<AddressItemApiModel> Addresses { get; } = new List<AddressItemApiModel>();

        public Dictionary<string, string> Countries { get; } = new Dictionary<string, string>();

        public virtual void Initialize(IEnumerable<IParty> addresses, Dictionary<string, string> countries)
        {
            Assert.ArgumentNotNull(addresses, nameof(addresses));

            if (countries != null && countries.Count > 0)
            {
                Countries.AddRange(countries);
            }

            var addressArray = addresses as IParty[] ?? addresses.ToArray();
            foreach (var address in addressArray)
            {
                var result = new AddressItemApiModel();
                result.Initialize(address);
                Addresses.Add(result);
            }
        }
    }
}