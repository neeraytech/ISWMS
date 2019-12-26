"use strict";
var KTDatatablesBasicPaginations = function() {

	var initTable1 = function() {
		var table = $('#kt_table_1');

		// begin first table
		table.DataTable({
			responsive: true,
			pagingType: 'full_numbers',
			columnDefs: [
				{
					targets: -1,
					title: 'Actions',
					orderable: false,
					render: function(data, type, full, meta) {
						return `
                        <a href="#" class="btn btn-sm btn-secondary btn-bold" title=""><span class="kt-font-primary">Edit</span></a>
                        <a href="#" class="btn btn-sm btn-secondary btn-bold" title=""><span class="kt-font-danger">Delete</span></a>
                        `;
					},
				},
//				{
//					targets: 8,
//					render: function(data, type, full, meta) {
//						var status = {
//							1: {'title': 'Pending', 'class': 'kt-badge--brand'},
//							2: {'title': 'Delivered', 'class': ' kt-badge--metal'},
//							3: {'title': 'Canceled', 'class': ' kt-badge--primary'},
//							4: {'title': 'Success', 'class': ' kt-badge--success'},
//							5: {'title': 'Info', 'class': ' kt-badge--info'},
//							6: {'title': 'Danger', 'class': ' kt-badge--danger'},
//							7: {'title': 'Warning', 'class': ' kt-badge--warning'},
//						};
//						if (typeof status[data] === 'undefined') {
//							return data;
//						}
//						return '<span class="kt-badge ' + status[data].class + ' kt-badge--inline kt-badge--pill">' + status[data].title + '</span>';
//					},
//				},
//				{
//					targets: 9,
//					render: function(data, type, full, meta) {
//						var status = {
//							1: {'title': 'Online', 'state': 'danger'},
//							2: {'title': 'Retail', 'state': 'primary'},
//							3: {'title': 'Direct', 'state': 'accent'},
//						};
//						if (typeof status[data] === 'undefined') {
//							return data;
//						}
//						return '<span class="kt-badge kt-badge--' + status[data].state + ' kt-badge--dot"></span>&nbsp;' +
//							'<span class="kt-font-bold kt-font-' + status[data].state + '">' + status[data].title + '</span>';
//					},
//				},
			],
		});
	};

	return {

		//main function to initiate the module
		init: function() {
			initTable1();
		}
	};
}();

jQuery(document).ready(function() {
	KTDatatablesBasicPaginations.init();
});