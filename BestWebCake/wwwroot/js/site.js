// Write your Javascript code.
var outImage ="Cake_Image";
function preview_2(obj)
{
	if (FileReader)
	{
		var reader = new FileReader();
		reader.readAsDataURL(obj.files[0]);
		reader.onload = function (e) {
		var image=new Image();
		image.src=e.target.result;
		image.onload = function () {
			document.getElementById(outImage).src=image.src;
		};
		}
	}
	else
	{
		    // Not supported
	}
}