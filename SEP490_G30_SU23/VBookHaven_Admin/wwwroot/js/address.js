const host = "https://provinces.open-api.vn/api/";
var callAPI = (api) => {
    var selectedProvince = $("#province").val();
    return axios.get(api)
        .then((response) => {
            renderData(response.data, "province");
            $("#province").val(selectedProvince);
        });
}
callAPI('https://provinces.open-api.vn/api/?depth=1').then(() => {
    // Gọi hàm callApiDistrict và callApiWard với giá trị mặc định
    callApiDistrictDefault(host + "p/" + $("#province").val() + "?depth=2");
    callApiWardDefault(host + "d/" + $("#district").val() + "?depth=2");
});


var callApiDistrict = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.districts, "district");
        });
}
var callApiWard = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.wards, "ward");
        });
}

var callApiDistrictDefault = (api) => {
    var selectedDistrict = $("#district").val();
    return axios.get(api)
        .then((response) => {
            renderData(response.data.districts, "district");
            $("#district").val(selectedDistrict);
        });

}
var callApiWardDefault = (api) => {
    var selectedWard = $("#ward").val();
    return axios.get(api)
        .then((response) => {
            renderData(response.data.wards, "ward");
            $("#ward").val(selectedWard);
        });
}

var renderData = (array, select) => {
    let row = '';
    if (select === 'province') {
        row = '<option disabled selected >Chọn Tỉnh/Thành phố</option>';
    } else if (select === 'district') {
        row = '<option disabled selected >Chọn Quận/Huyện</option>';
    } else {
        row = '<option disabled selected >Chọn Phường/Xã</option>';
    }
    array.forEach(element => {
        row += `<option value="${element.code}">${element.name}</option>`
    });
    document.querySelector("#" + select).innerHTML = row
}

$("#province").change(() => {
    callApiDistrict(host + "p/" + $("#province").val() + "?depth=2");
    $('#ward').empty().append('<option value="">Chọn Phường/Xã</option>');
    $('#district').empty().append('<option value="">Chọn Quận/Huyện</option>');
    clearResult();
});
$("#district").change(() => {
    callApiWard(host + "d/" + $("#district").val() + "?depth=2");
    $('#ward').empty().append('<option value="">Chọn Phường/Xã</option>');
    clearResult();
});
$("#ward").change(() => {
    saveResult();
});


$('.single-select').select2({
    theme: 'bootstrap4',
    width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
    placeholder: $(this).data('placeholder'),
    allowClear: Boolean($(this).data('allow-clear')),
});

var saveResult = () => {
    let provinceString = "";
    let districtString = "";
    let wardString = "";
    ////to print all value
    if ($("#district").val() != "" && $("#province").val() != "" &&
        $("#ward").val() != "") {
        ////print value
        //let result = $("#ward option:selected").text() +
        //    " | " + $("#district option:selected").text() +
        //    " | " + $("#province option:selected").text();
        //$("#result").val(result)

        //save value
         provinceString += $("#province option:selected").text();
         $("#provinceString").val(provinceString);
         districtString += $("#district option:selected").text();
         $("#districtString").val(districtString);
         wardString += $("#ward option:selected").text();
         $("#wardString").val(wardString);

    }

    //let provinceString = "";
    //let districtString = "";
    //let wardString = "";
    // if ($("#province").val() != "") {
    //     provinceString = $("#province option:selected").text();
    //     $("#provinceString").val(provinceString);
    // }
    // if ($("#district").val() != "") {
    //     districtString = $("#district option:selected").text();
    //     $("#districtString").val(districtString);
    // }
    // if ($("#ward").val() != "") {
    //     wardString = $("#ward option:selected").text();
    //     $("#wardString").val(wardString);
    // }
}
var clearResult = () => {
    $("#provinceString").val("");
    $("#districtString").val("");
    $("#wardString").val("");
}

function goBack() {
    history.back();
}