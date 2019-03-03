function tableInitComplete() {
    this.api()
        .columns([3, 4])
        .every(function (index) {

            var column = this;
            if (index === 3) {
                var select = $('<select class="form-control"><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change',
                    function () {
                        var val = $.fn.dataTable.util.escapeRegex($(this).val());
                        column.search(val, false, false).draw();
                    });

                var levels = ['Verbose', 'Debug', 'Information', 'Warning', 'Error', 'Fatal'];
                levels.forEach(function (item) {
                    select.append('<option value="' + item + '">' + item + '</option>');
                });
            }
            if (index === 4) {
                moment.locale('hu-HU');

                $('<input class="form-control" type="text"/>')
                    .appendTo($(column.footer()).empty())
                    .daterangepicker({
                        "autoUpdateInput": false,
                        "showDropdowns": true,
                        "showISOWeekNumbers": true,
                        "locale": {
                            "format": "YYYY.MM.DD",
                            "separator": " - ",
                            "applyLabel": "Kiválaszt",
                            "cancelLabel": "Törlés",
                            "fromLabel": "Tól",
                            "toLabel": "Ig",
                            "customRangeLabel": "Egyedi",
                            "weekLabel": "H",
                            "daysOfWeek": moment.weekdaysMin(),
                            "monthNames": moment.months(),
                            "firstDay": 1
                        },
                        "linkedCalendars": true,
                        "opens": "center"
                    })
                    .on('apply.daterangepicker',
                    function (ev, picker) {
                        var val = picker.startDate.format('YYYY.MM.DD') + ' - ' + picker.endDate.format('YYYY.MM.DD');
                        $(this).val(val);
                        column.search(val, false, false).draw();
                    })
                    .on('cancel.daterangepicker',
                    function (ev, picker) {
                        $(this).val('');
                        column.search('', false, false).draw();
                    })
                    .on('keydown', function (ev) {
                        if (ev.keyCode === 13) {
                            column.search(this.value).draw();
                        }
                    });
            }
        });
}