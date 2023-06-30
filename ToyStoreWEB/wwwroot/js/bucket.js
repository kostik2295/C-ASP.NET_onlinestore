const tableRows = document.querySelectorAll('tr');

tableRows.forEach(function (row) {
    const quantityInput = row.querySelector('td.quantity-input');
    const priceCell = row.querySelector('td.price');
    const totalPriceCell = row.querySelector('td.total-price');
    const deleteButton = row.querySelector('td.delete-button');

    // Получение начального значения стоимости и количества
    const initialPrice = parseFloat(priceCell.textContent);

    // Обработчик изменения значения input
    quantityInput.addEventListener('change', function () {
        const quantity = parseInt(quantityInput.value);
        const totalPrice = initialPrice * quantity;

        // Обновление значения стоимости в 4-м столбце
        totalPriceCell.textContent = totalPrice + ' р.';
    });

    // Обработчик нажатия на кнопку удаления
    deleteButton.addEventListener('click', function () {
        // Удаление элемента из списка или выполнение других действий
        row.remove(); // Удаляет строку из таблицы
    });
});