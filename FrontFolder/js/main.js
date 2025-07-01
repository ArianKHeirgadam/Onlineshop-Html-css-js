document.addEventListener('DOMContentLoaded', function() {
    
    console.log('JavaScript is working!');
    
    
    document.body.classList.add('bg-gray-100');
}); 
let trCount = 0;
let tdCount = 0;

function generateCard(p) {
    let table = document.getElementById('main');
    let tr;

    if (tdCount === 0) {
        tr = document.createElement('tr');
        tr.id = 'tr' + trCount;
        table.appendChild(tr);
    } else {
        tr = document.getElementById('tr' + trCount);
    }

    let encoded = encodeURIComponent(JSON.stringify(p));
    tr.innerHTML +=` 
        <td>
            <div class="uk-card uk-card-default uk-card-hover" 
                 style="width: 15vw; cursor: pointer;" 
                 data-product='${encoded}' 
                 onclick="handleCardClick(this)">
                <div class="uk-card-media-top">
                    <img src="resource/${p.image}" style="height: 15vw" alt="">
                </div>
                <div class="uk-card-body uk-padding-small">
                    <h3 class="uk-card-title">${p.name}</h3>
                    <p>${p.brandName}</p>
                    <div class="uk-flex uk-flex-middle uk-flex-between">
                        <p class="uk-margin-remove">${p.price} تومان</p>
                        <span class="uk-badge">${p.rating}</span>
                    </div>
                </div>
            </div>
        </td>
    `;

    tdCount++;
    if (tdCount === 4) {
        tdCount = 0;
        trCount++;
    }
}

function addToCart(productId) {
    
    console.log('Adding product to cart:', productId);

}
function showModal(p) {
    var s = String(p.price).trim();
    var result = '';
    var count = 0;
    for (var i = s.length - 1; i >= 0; i--) {
        if (count % 3 == 0 && count !== 0) {
            result = ',' + result;
        }
        result = s[i] + result;
        count++;
    }
 
    var modalHTML = `
        <img src="resource/${p.image}" class="uk-width-1-1 uk-border-rounded" alt="${p.name}">
        <h3 class="uk-margin-small-top">${p.name}</h3>
        <p><strong>برند:</strong> ${p.brandName}</p>
        <p><strong>توضیحات:</strong> ${p.description || 'اطلاعاتی موجود نیست.'}</p>
        <div class="uk-flex uk-flex-between uk-margin-top">
            <span><strong>قیمت:</strong> ${result} تومان</span>
            <span class="uk-badge uk-badge-success">${p.rating}</span>
        </div>
    `;

    document.getElementById('modal-content').innerHTML = modalHTML;
    
    UIkit.modal('#product-modal').show();
}
function handleCardClick(element) {
    let product = JSON.parse(decodeURIComponent(element.getAttribute('data-product')));
    showModal(product);
}
