        $('#btnSelectImage').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#txtImage').val(url);
            };
            finder.popup();
        })

        var editor = CKEDITOR.replace('txtContent', {
            customConfig: '~/Assets/admin/js/plugins/ckeditor/config.js',
        });