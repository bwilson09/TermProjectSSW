namespace TermProject.ViewModels
{
    public class AdminWrapperVm
    {
        //wrapper vm class needed in order to access mutliple view models per page
        public FilterVm Filter { get; set; } = new FilterVm();
        public IEnumerable<AdminIndexVm> Teams { get; set; } = new List<AdminIndexVm>();
    }
}
