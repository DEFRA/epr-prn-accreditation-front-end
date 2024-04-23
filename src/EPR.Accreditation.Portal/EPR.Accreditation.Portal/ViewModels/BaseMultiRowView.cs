namespace EPR.Accreditation.Portal.ViewModels
{
    using EPR.Accreditation.Portal.Attributes.Validation;
    using EPR.Accreditation.Portal.Resources;
    using EPR.Accreditation.Portal.ViewModels.Interfaces;

    /// <summary>
    /// Base class for view where there are multiple rows that can be added
    /// </summary>
    /// <typeparam name="T">The type that represents the rows in the view model</typeparam>
    public abstract class BaseMultiRowView<T>
        where T : IEntryMade
    {
        /// <summary>
        /// Gets or sets the Rows that represent Rows in the view that contain the actual
        /// data.
        /// </summary>
        [ListMustBePopulated(ErrorMessageResourceName = "AtLeastOneEntryRequired", ErrorMessageResourceType = typeof(NonWasteInputLastYearResources))]
        public IList<T> Rows { get; set; }

        /// <summary>
        /// Gets or sets a value to help identify how many extra rows to add over the initial 3 when
        /// adding rows without javascript.
        /// </summary>
        public int RowsToAdd { get; set; }

        /// <summary>
        /// Gets the number of rows that have been populated.
        /// </summary>
        /// <param name="initialRowsToDisplay">The number of rows to display intially</param>
        /// <returns>The number of rows to display</returns>
        public int RowsToDisplay(int initialRowsToDisplay) =>
            Rows
            .Select((entry, index) => new
            {
                Entry = entry,
                Index = index,
            })
            .Where(r => r.Entry.EntryMade)
            .Select(e => e.Index + 1)
            .DefaultIfEmpty()
            .Max() <= initialRowsToDisplay ? initialRowsToDisplay + RowsToAdd : Rows.Count;
    }
}
