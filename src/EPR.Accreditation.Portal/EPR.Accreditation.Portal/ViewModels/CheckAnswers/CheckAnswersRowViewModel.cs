namespace EPR.Accreditation.Portal.ViewModels.CheckAnswers
{
    public class CheckAnswersRowViewModel
    {
        public string ListKey { get; set; }

        public string ListValue { get; set; }

        public string Controller { get; set; }

        public string ControllerAction { get; set; }
        
        public string Area { get; set; }
        
        public IDictionary<string, string> RouteData { get; set; }
        
	    public IDictionary<string, string> RouteAction { get; set; }      
    }
}
