using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using SimpleProjectManager.Module.BusinessObjects;

namespace SimpleProjectManager.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ProjectTaskController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/

        public ProjectTaskController()
        {
            // Specify the type of objects that can use the Controller.
            TargetObjectType = typeof(ProjectTask);
            // Activate the Controller in any type of View.
            TargetViewType = ViewType.Any;

            SimpleAction markCompletedAction = new SimpleAction(this, "MarcarComoConcluida",
                DevExpress.Persistent.Base.PredefinedCategory.RecordEdit)
            {
                TargetObjectsCriteria = (
                                            CriteriaOperator.FromLambda<ProjectTask>(t => t.Status != ProjectTaskStatus.Completed)
                                        ).ToString(),
                ConfirmationMessage =
                "Tem certeza que deseja marcar as tarefas como concluídas?",
                ImageName = "State_Task_Completed"
            };

            markCompletedAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;

            markCompletedAction.Execute += (s, e) =>
            {
                foreach (ProjectTask task in e.SelectedObjects)
                {
                    task.EndDate = DateTime.Now;
                    task.Status = ProjectTaskStatus.Completed;
                    View.ObjectSpace.SetModified(task);
                }
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            };
        }



        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
