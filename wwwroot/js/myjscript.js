function fileupload(filename)
{
    var inputfile = document.getElementById(filename);
    var files = inputfile.files;
    var data = new FormData();

    for (var i = 0; i != files.length; i++){

        data.append("files", files[i])
    }
    $.ajax(

        {
            url:"/home",
            data: fdata, 
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                alert("File Upload Successfully")
            }
            
        }


    );
}