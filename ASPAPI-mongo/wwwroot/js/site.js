let nameArray = [];
let originArray = [];
let genderArray = [];

let Name = function (pName, pOriginId, pGenderId, pYearMost, pYearLeast) {

    this.name = pName;
    this.originId = parseInt(pOriginId);
    this.genderId = parseInt(pGenderId);
    this.yearMost = pYearMost;
    this.yearLeast = pYearLeast;
}
let Origin = function (pUsage) {

    this.usage = pUsage;

}
let Gender = function (pGender1) {

    this.gender1 = pGender1;

}


document.addEventListener("DOMContentLoaded", function () {
    updateNameDisplay();
    updateOriginList();
    updateGenderList();


});

function updateNameDisplay() {

    $.get("api/Name", function (data, status) {  // AJAX get
        nameArray = data;  // put the returned server json data into our local array

        let table = document.getElementById('nametable');

        while (table.rows.length > 0) {
            table.deleteRow(0);
        }
        let tr = document.createElement('tr');
        let td1 = document.createElement('td');
        td1.textContent = "Name";
        tr.appendChild(td1);
        let td2 = document.createElement('td');
        td2.textContent = "Gender";
        tr.appendChild(td2);
        let td3 = document.createElement('td');
        td3.textContent = "Origin";
        tr.appendChild(td3);
        let td4 = document.createElement('td');
        td4.textContent = "Year Most Popular";

        tr.appendChild(td4);
        let td5 = document.createElement('td');
        td5.textContent = "Year Least Popular";

        tr.appendChild(td5);


        table.appendChild(tr);


        for (let item of nameArray) {
            let tr = document.createElement('tr');

            let td1 = document.createElement('td');
            td1.textContent = item.name;
            tr.appendChild(td1);

            let td2 = document.createElement('td');
            td2.textContent = item.gender1;
            tr.appendChild(td2);

            let td3 = document.createElement('td');
            td3.textContent = item.usage;
            tr.appendChild(td3);

            let td4 = document.createElement('td');
            td4.textContent = item.yearMost;
            tr.appendChild(td4);
            let td5 = document.createElement('td');
            td5.textContent = item.yearLeast;
            tr.appendChild(td5);

            table.appendChild(tr);
        }

        console.log(nameArray);
    });
}




//button for new country of origin
function newOriginButton() {
    let newOrigin = document.getElementById("newOrigin").value;

     console.log(newOrigin);
    $.ajax({
        url: "api/Origin",
        type: "POST",
        data: JSON.stringify(newOrigin),
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            console.log(result);
            document.getElementById("originList").value = "";
            updateOriginList();
        }
    })

}
// update origin of the country list
function updateOriginList() {
    $.get("api/Origin", function (data, status) {
        let originSelect = document.getElementById('originList');
        originArray = data;
        while (originSelect.options.length > 0) {
            originSelect.remove(0);
        }
        for (let item of originArray) {
            let newOption = document.createElement('option');
            let optionText = document.createTextNode(item.usage);
            newOption.appendChild(optionText)

            newOption.setAttribute("value", item.originId);
            originSelect.appendChild(newOption);
        }
    });


}
// add new gender to the list
function newGenderButton() {
    let newGender = document.getElementById("newGender").value;

     console.log(newGender);
    $.ajax({
        url: "api/Gender",
        type: "POST",
        data: JSON.stringify(newGender),
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            console.log(result);
            document.getElementById("genderList").value = "";
            updateGenderList();
        }
    })
}
//update gender list
    function updateGenderList() {
        $.get("api/Gender", function (data, status) {
            let genderSelect = document.getElementById('genderList');
            genderArray = data;
            console.log(data);
            while (genderSelect.options.length > 0) {
                genderSelect.remove(0);
            }
            for (let item of genderArray) {
                let newOption = document.createElement('option');
                let optionText = document.createTextNode(item.gender1);
                newOption.appendChild(optionText)

                newOption.setAttribute("value", item.genderId);
                genderSelect.appendChild(newOption);
            }
        });

    
}
//add new name button
function addButton() {
    let originSelect = document.getElementById('originList');
    let genderSelect = document.getElementById('genderList');

    let newName = document.getElementById("name").value;
    console.log(newName);
    let newOriginId = parseInt(originSelect.value);
  /*  console.log(newOriginId);*/
    let newGenderId = parseInt(genderSelect.value);

    let newMostYear = document.getElementById("mostYear").value;
    let newLeastYear = document.getElementById("leastYear").value;
    let newList = new Name(newName, newOriginId, newGenderId,newMostYear,newLeastYear);
    console.log(newList);

    $.ajax({
        url: "api/Name",
        type: "POST",
        data: JSON.stringify(newList),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            document.getElementById("name").value = "";
            document.getElementById("mostYear").value = "";
            document.getElementById("leastYear").value = "";
            console.log(result);
            updateNameDisplay();
        }
    });

}

function showButton(){
    updateNameDisplay()
}




let MongoNames = function (pName, pGender1, pUsage, pYearMost, pYearLeast) {
    this.name = pName;
    this.gender1 = pGender1;
    this.usage = pUsage;
    this.yearMost = parseInt(pYearMost);
    this.yearLeast = parseInt(pYearLeast);
}


function backupButton() {    // get all names in mongo
    $.get("api/MongoNames", function (data, status) {
        let nameArray = data;
        for (let aName of nameArray) {  // delete one at a time
            $.ajax({
                url: "api/MongoNames/" + aName.id,
                type: "DELETE",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                }
            });
        };
    });

    $.get("api/Name", function (data, status) { // get all names from SQL
        let mongoArray = data;
        for (let aName of mongoArray) {
            let nextMongoName = new MongoNames(aName.name, aName.gender1, aName.usage, aName.yearMost, aName.yearLeast);
            $.ajax({
                url: "api/MongoNames",
                type: "POST",
                data: JSON.stringify(nextMongoName),
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    console.log(result);
                }
            });
        }
    });
}

function displayBackupButton() {
    $.get("api/MongoNames", function (data, status) {
        let nameArray = data;
        let table = document.getElementById('nametable');
        while (table.rows.length > 0) {      // clear old values first
            table.deleteRow(0);
        }
        let tr = document.createElement('tr');   // create table and headers
        let td1 = document.createElement('td');
        td1.textContent = "Backup Data";
        tr.appendChild(td1);
        table.appendChild(tr);
        for (let aName of nameArray) {      // add rows
            let tr = document.createElement('tr');
            let td1 = document.createElement('td');
            td1.textContent = aName.name;
            tr.appendChild(td1);
            let td2 = document.createElement('td');
            td2.textContent = aName.gender1;
            tr.appendChild(td2);
            let td3 = document.createElement('td');
            td3.textContent = aName.usage;
            tr.appendChild(td3);
            let td4 = document.createElement('td');
            td4.textContent = aName.yearMost;
            tr.appendChild(td4);
            let td5 = document.createElement('td');
            td5.textContent = aName.yearLeast;
            tr.appendChild(td5);
            table.appendChild(tr);
        }
    });
}