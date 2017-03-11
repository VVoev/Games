window.onload = function () {

    function getClueForTheWord() {
        var chosenCategoryIndex = categories.indexOf(chosenCategory);
        var hintIDex = chosenCategory.indexOf(word);
        var help = categoriesHint[chosenCategoryIndex][hintIDex];
        return help;
    }
    var defaultNumberOfLifes = 10;

    var alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
        'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
        't', 'u', 'v', 'w', 'x', 'y', 'z'];

    var categories =[
                    ['Levski','Cska','Beroe','Litex','Botev','Slavia'],
                    ['Finding Nemo','Gladiator','Gone in sixty Seconds','The Patriot','Home Alone'],
                    ['C++','C#','JavaScript','Python','Perl']
                    ];

    var categoriesHint = [
               ['Named on a famous bulgarian','All Matches play with Red','From Stara Zagora','From Dobrich','From Plovdiv','White Canarcheta'],
               ['Fish Adventure','Best movie with Rusel Crowl','Anjelina Jolie And Nickalas Cage','No categoriesHint','Best Chrismats movie'],
               ['Fastest language','Microsoft language','Chrome Language','Snake Language ;)','Sea Language']
    ];

    //create alphabet UL
    var myButtons = document.getElementById('buttons');
    var letters = document.createElement('ul');
    for(var i = 0,len = alphabet.length; i<len;i+=1){
        letters.id = 'alphabet';
        var list = document.createElement('li');
        list.id = 'letter';
        list.innerHTML = alphabet[i];
        myButtons.appendChild(letters);
        letters.appendChild(list);
    }


    //Select Category
    var selectedCat  = function () {
        if(chosenCategory === categories[0]){
            categoryName.innerHTML = 'The Chosen Category is Football';
        }
        else if(chosenCategory === categories[1]){
            categoryName.innerHTML = 'The Chosen Category is Movies';
        }
        else if(chosenCategory === categories[2]){
            categoryName.innerHTML = 'The Chosen Category is Programing Languages';
        }
    }



    //Choose Category and word and clue
    chosenCategory = categories[Math.floor(Math.random()*categories.length)];
    word = chosenCategory[Math.floor(Math.random()*chosenCategory.length)];
    clue = getClueForTheWord();
    
    
    //showClue
    var hintButton = document.getElementById('hint');
    hintButton.addEventListener('click',function () {
        var clueElement = document.getElementById('clue');
        clueElement.innerText +=clue;
    })

    //Play Again Button
    var reset = document.getElementById('reset');
    reset.addEventListener('click',function () {
        location.reload();
    })

    //Set Starting Lifes
    var lifes = document.getElementById('mylives');
    lifes.innerText += defaultNumberOfLifes;
    

    
    //make Lines for the word
    var guess = function () {
        var hold = document.getElementById('hold');
        var ul = document.createElement('ul');

        for(var i in word){
            var li = document.createElement('li');
            li.setAttribute('class', 'guess');
            li.innerHTML='_';
            ul.appendChild(li);
        }
        hold.appendChild(ul);
    }

    //Debugging purposes
    selectedCat();
    console.log(word)
    guess();
    
    







}



