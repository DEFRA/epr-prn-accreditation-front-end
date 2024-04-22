export default (function () {
    $('.add-row').on('click', function (e) {
        var functionName = $(this).data('function');

        // turn off the default behaviour of the button
        e.preventDefault();

        var maxRows = parseInt($('#maxRows').val());
        var currentRows = addRowFunctions[functionName](maxRows);

        // if number of rows after adding a new row is 5, then this button should be removed
        if (currentRows + 1 >= maxRows) {
            $(this).remove();
        }
    });

    var addRowFunctions = {
        reprocessorSupportingInformation: function (maxRows) {
            var tonnesLabel = $('#tonnesLabel').val();

            // need to count the number of existing rows - this is so that we can set the indexes correctly
            // since indexes are zero based, the length will be the index for the next row
            var currentRows = $('form .govuk-form-group fieldset.govuk-fieldset > .govuk-form-group').length;

            var html =
                '<div class="govuk-form-group">' +
                    '<div class="govuk-input__wrapper">' +
                        `<input class="govuk-input govuk-input--width-10 govuk-!-margin-right-4" type="text" name="Rows[${currentRows}].Type">` +
                        '<div class="govuk-input__wrapper">' +
                            `<input class="govuk-input govuk-input--width-10 3dp" type="text" name="Rows[${currentRows}].Tonnes" autocomplete="off" />` +
                            `<div class="govuk-input__suffix" aria-hidden="true">${tonnesLabel}</div>` +
                        '</div >' +
                    '</div > ' +
                '</div > ';
            $('form .govuk-form-group fieldset.govuk-fieldset').append(html);

            return currentRows;
        },

        wasteDescription: function (maxRows) {
            // need to count the number of existing rows - this is so that we can set the indexes correctly
            // since indexes are zero based, the length will be the index for the next row
            var currentRows = $('form .govuk-form-group fieldset.govuk-fieldset > .govuk-form-group').length;

            var html =
                '<div class="govuk-form-group">' +
                '<div class="govuk-input__wrapper">' +
                `<input class="govuk-input govuk-input--width-10 govuk-!-margin-right-4" type="text" asp-for="Rows[${currentRows}].WasteDescriptionCode">` +
                '</div>' +
                '</div>';
            $('form .govuk-form-group fieldset.govuk-fieldset').append(html);

            return currentRows;
        }
    }
})();