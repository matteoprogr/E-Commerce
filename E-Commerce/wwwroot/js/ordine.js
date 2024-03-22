

let Select = document.getElementById("idCategoria");
allProducts();
Select.onchange = () => {
    let idC = Select.value;
    
    
    loadProdotti(idC);
}
function loadProdotti(id) {
    fetch("/Ordini/Prodotti", {
        method: "post",
        headers: {
            "Accept": "application/json",
            "Content-type": "application/json"
        },
        body: JSON.stringify(id)
    }).then(response => response.json()).then(data => {
        
        popolaTabella(data)
        console.log(data);
    })
}



function popolaTabella(data) {
    var table = document.getElementById('MyTable');
    var tbody = table.querySelector('tbody');
    let i = 0;

    // Svuota il corpo della tabella prima di popolarlo
    tbody.innerHTML = "";

    // Popola la tabella con i dati dalla risposta
    data.forEach(item => {

        var tr = document.createElement('tr');

        // Popola le colonne della tua tabella
        var td1 = document.createElement('td');
        td1.id = "td1" + i;
        
        td1.appendChild(document.createTextNode(item.nome));
        tr.appendChild(td1);

        var td2 = document.createElement('td');
        td2.id = "td2" + i;
        td2.appendChild(document.createTextNode(item.quantita));
        tr.appendChild(td2);

        var td3 = document.createElement('td');
        td3.id = "td3" + i;
        td3.appendChild(document.createTextNode(item.prezzo));
        tr.appendChild(td3);

        var td4 = document.createElement('td');
        var qs = document.createElement('input');
        qs.classList.add("inputClass");
        qs.type = 'number';
        qs.value = 0;
        qs.id = "td4" + i;
        td4.appendChild(qs);
        tr.appendChild(td4);

        var td5 = document.createElement('td');
        var bottone = document.createElement('input');
        bottone.type = "submit"
        bottone.id = i;
        bottone.value = "aggiungi al carrello";

        bottone.addEventListener("click", function () { carrello(bottone.id)});
        td5.appendChild(bottone);
        tr.appendChild(td5);

        tbody.appendChild(tr);
        i++;
    });
}
function carrello(id) {

    td1 = document.getElementById("td1" + id);
    let td1Value = td1.childNodes[0].nodeValue.trim();
    td2 = document.getElementById("td2" + id);
    let td2Value = td2.childNodes[0].nodeValue.trim();
    td3 = document.getElementById("td3" + id);
    let td3Value = td3.childNodes[0].nodeValue.trim();
    td4 = document.getElementById("td4" + id).value;
  
    dati = {
        Nome : td1Value,
        Quantita: td2Value,
        Prezzo: td3Value,
        QuantitaSelezionata: td4
    }
    console.log(dati);
    fetch("/Ordini/Carrello", {
        method: "post",
        headers: {
            "Accept": "application/json",
            "Content-type": "application/json"
        },
        body: JSON.stringify(dati)
        
    })
   
}

function allProducts() {

    fetch("/Ordini/AllProducts", {
    }).then(response => response.json()).then(data => {
        popolaTabella(data);
        console.log(data);
    })
}

    
   
