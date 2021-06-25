function onSearch(e) {
    if (e.key === "Enter") {
        location.href = `/Laptops/Index?search=${e.target.value}`;
    }
}
function ChoosenUrlImg() {
    $("#urlimage").addClass("d-block");
    $("#localImg").removeClass("d-block");

}
function ChoosenLocalImg() {
    $("#urlimage").removeClass("d-block");
    $("#localImg").addClass("d-block");
}

function setFilter(element) {
    var type = $(element).data("type");
    var value = $(element).val();

    $("#laptopsContainer").load(`/Laptops/Filter?type=${type}&value=${encodeURIComponent(value)}`);

}

function DeleteOrder(id) {
    if (confirm("Do yo really want to delete this order?"))
        window.location = `/Laptops/DeleteOrder?id=` + id;
}