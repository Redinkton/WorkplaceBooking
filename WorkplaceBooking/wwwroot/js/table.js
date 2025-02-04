const seats = document.querySelectorAll('.seat');

// Обработка клика по месту
seats.forEach(seat => {
    seat.addEventListener('click', async () => {
        const seatNumber = parseInt(seat.getAttribute('data-seat'));

        if (seat.classList.contains('occupied')) {
            alert(`Место ${seatNumber} уже занято!`);
        } else {
            // Отправка запроса на сервер для бронирования места
            const response = await fetch(`/Table?handler=Reserve&seatNumber=${seatNumber}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                }
            });

            const result = await response.json();
            if (result.success) {
                alert(`Место ${seatNumber} успешно забронировано!`);
                seat.classList.add('occupied');
            } else {
                alert(result.message || 'Ошибка при бронировании места.');
            }
        }
    });
});
document.querySelectorAll('.seat').forEach(seat => {
    seat.addEventListener('click', function() {
        alert('Вы выбрали место №' + this.dataset.seat);
    });
});

