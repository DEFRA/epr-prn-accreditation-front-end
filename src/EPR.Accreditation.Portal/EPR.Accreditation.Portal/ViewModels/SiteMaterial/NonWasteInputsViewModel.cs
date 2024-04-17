namespace EPR.Accreditation.Portal.ViewModels.SiteMaterial
{
    /// <summary>
    /// View model for Non Waste Inputs. Shared by estimated or last calender year.
    /// </summary>
    public class NonWasteInputsViewModel : BaseMultiRowView<TypeTonnesRowViewModel>
    {
        /// <summary>
        /// Gets or sets the Id of the accreditation populated from the route values.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Id for the current material populated from the route values.
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether waste was processed last year.
        /// This is required so that we know which view to return.
        /// </summary>
        public bool? WasteLastYear { get; set; }

        /*
                /// <summary>
                /// Gets or sets the Rows that represent Rows in the view that contain the actual
                /// data.
                /// </summary>
                [ListMustBePopulated(ErrorMessageResourceName = "AtLeastOneEntryRequired", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
                public IList<NonWasteInputsRowViewModel> Rows { get; set; }

                /// <summary>
                /// Gets or sets a value to help identify how many extra rows to add over the initial 3 when
                /// adding rows without javascript.
                /// </summary>
                public int RowsToAdd { get; set; }
        */
    }
}