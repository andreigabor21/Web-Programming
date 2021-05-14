function populate(s1, s2) {
    s1 = document.getElementById(s1);
    s2 = document.getElementById(s2);
    s2.innerHTML = "";
    let optionArray = [];

    if (s1.value === "cluj")
        optionArray = ["Gilau", "Sopor", "Mociu", "Frata", "Maguri-Racatau"];
    else if (s1.value === "botosani")
        optionArray = ["Botosani", "Frumusica", "Voiniceni", "Adjudeni", "Buruienesti"];
    else if (s1.value === "salaj")
        optionArray = ["Sanmihaiu Almasului", "Zalau", "Zimbor", "Hereclean"]
    else if (s1.value === "suceava")
        optionArray = ["Dumbraveni", "Fundu Moldovei", "Mitocu Dragomirnei", "Partestii de Jos"]

    for (const option of optionArray) {
        const newOption = document.createElement("option");
        newOption.value = option.toLowerCase();
        newOption.innerHTML = option;
        s2.options.add(newOption);
    }
}
