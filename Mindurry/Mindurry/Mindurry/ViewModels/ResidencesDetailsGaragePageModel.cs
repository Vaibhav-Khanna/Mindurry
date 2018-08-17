using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.ViewModels
{
    public class ResidencesDetailsGaragePageModel : BasePageModel
    {
        public ResidenceModel Garage { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Garage = (ResidenceModel)initData;
        }
    }
}
