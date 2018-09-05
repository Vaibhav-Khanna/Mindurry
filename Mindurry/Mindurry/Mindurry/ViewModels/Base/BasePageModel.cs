using FreshMvvm;
using Mindurry.DataStore.Abstraction;
using Mindurry.DataStore.Abstraction.Stores;
using Mindurry.DataStore.Implementation;
using Mindurry.DataStore.Implementation.Stores;
using Xamarin.Forms;

namespace Mindurry.ViewModels.Base
{
    public class BasePageModel : FreshBasePageModel
    {

        //protected IUserDialogs Dialog = UserDialogs.Instance;


        public Command BackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();
        });


        private bool isbusy;
        public bool IsBusy
        {
            get { return isbusy; }
            set
            {
                isbusy = value;
                RaisePropertyChanged();
            }
        }

        private bool _showEmpty;
        public bool ShowEmpty
        {
            get { return _showEmpty; }
            set
            {
                _showEmpty = value;
                RaisePropertyChanged();
            }
        }

        private bool isloadmore;
        public bool IsLoadMore
        {
            get { return isloadmore; }
            set
            {
                isloadmore = value;
                RaisePropertyChanged();
            }
        }

        private bool isrefreshing;
        public bool IsRefreshing
        {
            get { return isrefreshing; }
            set
            {
                isrefreshing = value;
                RaisePropertyChanged();
            }
        }

        public string LoadingText { get; set; }


        private string isloadingtext = "Loading";
        public string IsLoadingText
        {
            get { return isloadingtext; }
            set
            {
                isloadingtext = value;
                RaisePropertyChanged();
            }
        }

        private bool isloading;
        public bool IsLoading
        {
            get { return isloading; }
            set
            {
                isloading = value;
                RaisePropertyChanged();
            }
        }


        public static void Init()
        {
            FreshIOC.Container.Register<IStoreManager, StoreManager>();
            FreshIOC.Container.Register<IApartmentStore, ApartmentStore>();
            FreshIOC.Container.Register<ICellarStore, CellarStore>();
            FreshIOC.Container.Register<ICollectSourceStore, CollectSourceStore>();
            FreshIOC.Container.Register<IContactCustomFieldSourceEntryStore, ContactCustomFieldSourceEntryStore>();
            FreshIOC.Container.Register<IContactCustomFieldSourceStore, ContactCustomFieldSourceStore>();
            FreshIOC.Container.Register<IContactCustomFieldStore, ContactCustomFieldStore>();
            FreshIOC.Container.Register<IContactStore, ContactStore>();
            FreshIOC.Container.Register<IContactSequenceStore, ContactSequenceStore>();
            FreshIOC.Container.Register<IDocumentMindurryStore, DocumentMindurryStore>();
            FreshIOC.Container.Register<IGarageStore, GarageStore>();
            FreshIOC.Container.Register<IGardenStore, GardenStore>();
            FreshIOC.Container.Register<INoteStore, NoteStore>();
            FreshIOC.Container.Register<IResidenceStore, ResidenceStore>();
            FreshIOC.Container.Register<ITerraceStore, TerraceStore>();
            FreshIOC.Container.Register<IUserFavoriteStore, UserFavoriteStore>();
            FreshIOC.Container.Register<IUserStore, UserStore>();
        }

        protected IStoreManager StoreManager { get; } = FreshIOC.Container.Resolve<IStoreManager>();
    }
}
