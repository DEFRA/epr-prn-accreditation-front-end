export default (function () {
    // only do this if validation is turned on
    if ($("jsValidationEnaled").val().toLowerCase() === "true") {
        var cancelSubmit = true;
        $('button[name="saveButton"][value="SaveAndComeBack"]').on('click', function (e) {
            if (cancelSubmit)
                e.preventDefault();
            $('input, select, textarea').each(function () {
                var el = $(this);

                if (el.rules()) {
                    el.rules('remove', 'required');
                }
            });

            if (cancelSubmit) {
                cancelSubmit = false;
                $(this).trigger('click');
            }
        });
    }
})();