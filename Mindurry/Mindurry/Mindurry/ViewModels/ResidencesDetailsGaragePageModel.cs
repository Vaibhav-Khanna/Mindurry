using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.ViewModels
{
    public class ResidencesDetailsGaragePageModel : BasePageModel
    {
        public Residence Garage { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Garage = (Residence)initData;
        }
    }
}
