﻿using Mindurry.DataModels;
using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class ResidencesDetailsGaragePageModel : BasePageModel
    {
        public GaragesListModel Garage { get; set; }

        public DocumentMindurry ReferencePlan { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            Garage = (GaragesListModel)initData;
            ReferencePlan = new DocumentMindurry();
           
            IEnumerable<DocumentMindurry> docs = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(Garage.Garage.Id, ReferenceKind.Garage.ToString().ToLower());
            if (docs != null && docs.Any())
            {
                IEnumerable<DocumentMindurry> referencePlanList = docs.Where(x => x.DocumentType == DocumentType.Initial.ToString().ToLower());
                if (referencePlanList != null && referencePlanList.Any())
                {
                    ReferencePlan = (referencePlanList.ToList())[0];
                }
            }

            
        }
        public Command LinkToContactCommand => new Command(async () =>
        {
            var contact = await StoreManager.ContactStore.GetItemAsync(Garage.Garage.ContactId);

            if (contact.Qualification == Qualification.Client.ToString())
            {
                await CoreMethods.PushPageModel<ClientsDetailsInfoPageModel>(Garage.Garage.ContactId);
            }
            else
            {
                await CoreMethods.PushPageModel<LeadDetailPageModel>(Garage.Garage.ContactId);
            }

        }
        );
    }
}
