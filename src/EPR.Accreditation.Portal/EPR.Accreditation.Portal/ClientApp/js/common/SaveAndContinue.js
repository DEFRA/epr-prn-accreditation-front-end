export default (function () {
    var cancelSubmit = true;
    $('button[name="saveButton"][value="SaveAndComeBack"]').on('click', function (e) {
        if (cancelSubmit)
            e.preventDefault();
        $('input, select, textarea').each(function () {
            $(this).rules('remove', 'required');
        });

        if (cancelSubmit) {
            cancelSubmit = false;
            $(this).trigger('click');
        }
    });
})();