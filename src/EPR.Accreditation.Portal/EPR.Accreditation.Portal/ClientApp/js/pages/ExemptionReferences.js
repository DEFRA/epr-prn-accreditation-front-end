export default (() => {
        $(document).ready(function () {
            function clearDuplicateReferenceNumber() {
                $("input[type='text'][data-clear-duplicate='true']").val('');
            }
        });
    })();