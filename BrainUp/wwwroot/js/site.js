// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.input-file input[type=file]').on('change', function () {
	let file = this.files[0];
	$(this).next().html(file.name);
});
