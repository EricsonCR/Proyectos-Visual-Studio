
//$(document).ready(function () {
//    $('#btn_buscar_estudiante').click(function () {
//        var dni_estudiante = $('#txt_dni_estudiante').val();
//        $.ajax({
//            type: "GET",
//            url: "/Estudiante/LeerEstudiantePorDni",
//            data: { dni: dni_estudiante },
//            success: function (result) {
//                $("#txt_id_estudiante").val(result.idkkk);
//                $("#txt_nombre_completo_estudiante").val(result.nombre + " " + result.apellido);
//            },
//            error: function (status, error) {
//                console.log(status + "-" + error)
//            }
//        });
//    });
//    $('#btn_buscar_apoderado').click(function () {
//        var dni_apoderado = $('#txt_dni_apoderado').val();
//        $.ajax({
//            type: "GET",
//            url: "/Apoderado/LeerApoderadoPorDni",
//            data: { dni: dni_apoderado },
//            success: function (result) {
//                $("#txt_id_apoderado").val(result.id);
//                $("#txt_nombre_completo_apoderado").val(result.nombre + " " + result.apellido);
//            },
//            error: function (status, error) {
//                console.log(status + "-" + error)
//            }
//        });
//    });
//});