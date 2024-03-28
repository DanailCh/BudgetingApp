
   
    const toggleButton = document.getElementById('PayButton');
    const hiddenSection = document.getElementById('hiddenSection');

   
    toggleButton.addEventListener('click', function () {
        
        if (hiddenSection.style.display === 'none') {
        hiddenSection.style.display = 'block';
        } else {
        hiddenSection.style.display = 'none';
        }
    });
