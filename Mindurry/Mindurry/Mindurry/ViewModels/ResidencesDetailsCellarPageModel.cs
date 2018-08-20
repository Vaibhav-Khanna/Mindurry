﻿using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindurry.ViewModels
{
    public class ResidencesDetailsCellarPageModel : BasePageModel
    {
        public CellarsListModel Cellar { get; set; }

        public DocumentMindurry ReferencePlan { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            Cellar = (CellarsListModel)initData;
            ReferencePlan = new DocumentMindurry();

            IEnumerable<DocumentMindurry> docs = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(Cellar.Id, ReferenceKind.Cellar.ToString().ToLower());
            if (docs != null && docs.Any())
            {
                IEnumerable<DocumentMindurry> referencePlanList = docs.Where(x => x.DocumentType == DocumentType.Initial.ToString().ToLower());
                if (referencePlanList != null && referencePlanList.Any())
                {
                    ReferencePlan = (referencePlanList.ToList())[0];
                }
            }


        }
    }
}
