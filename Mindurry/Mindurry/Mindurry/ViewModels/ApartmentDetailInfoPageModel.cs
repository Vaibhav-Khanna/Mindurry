using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;


namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ApartmentDetailInfoPageModel : BasePageModel
    {
        public Apartment Item { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);
            
            Item = (Apartment)initData;
          

        }
    }
}