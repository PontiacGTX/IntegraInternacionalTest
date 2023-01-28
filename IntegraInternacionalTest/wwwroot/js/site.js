// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var buttonSubmitEmpleado = document.getElementById('submitEmpleado');
buttonSubmitEmpleado.addEventListener('click', sendValues);
var imageFile = {};
const getVarName = varObj => Object.keys(varObj)[0]
function sendValues(event) {
    event.preventDefault();

    
    let formData = new FormData();
    for (const el of $('#formId').find(':input')) {
        
        console.log(el.name)
        if (el.type == 'file') {
            if (el.files.length > 0)
                formData.append('File', el.files[0], el.files[0].name)
            else
                formData.append('File',null)
        }
        else if (el.type!='button' || el.name != '__RequestVerificationToken') {
            if (el.name != '__RequestVerificationToken') {
                if(el.type != 'button')
                formData.append(el.name, el.value);
            }
        }

    }
    console.log(formData)
    for (var pair of formData.entries()) {
        console.log(pair[0] + ', ' + pair[1]);
    }
    let action = window.location.href.includes('Create') ? 'Create' : 'Edit';
    let route = `/Home/${action}`;
    if (action == 'Edit') {
        //var str = window.location.href;
        //let urlPath = str.split("/");
        //action = `${urlPath[3]}/${urlPath[4]}`;
        route = `/${action}`
    }
    $.ajax({
        type: 'POST',
        url: route,
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            
            console.log(result);
            if (result.statusCode != 200) {
                $('#validationMessages').empty()
                if (result.validation && result.validation.length > 0) {
                    $('#validationMessages').css('display','block')
                    $('#validationMessages').css('visibility', 'visible')
                    alert('Validation errors received');
                    for (const error of result.validation) {
                        for (const validation of error.value) {
                            $('#validationMessages').append('<li style="color:red">' + validation + ' </li>')
                        }
                    }
                    return;
                }
            }


            window.location.href = '/Home/Index';
        },
        error: function (a,xhr,c) {
            alert('Failed to receive the Data');
            console.log('Failed ' + xhr);
        }
    })
    console.log($('#formId').find(':input')[7]);

    return false;

}


function readFile(file) {
    var reader = new FileReader();
    reader.onload = readSuccess;
    function readSuccess(evt) {
        console.log(evt.target)
        $('#imageHolder').attr('src', evt.target.result).width(150).height(200)
    };
    reader.readAsDataURL(file);
}

document.getElementById('File').onchange = function (e) {
    
  
   
    readFile(e.srcElement.files[0]);
};