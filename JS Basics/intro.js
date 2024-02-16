let sentence = "I can eat bananas all day";
let word = sentence.slice(10, 17);
alert(word.toUpperCase());

let cars = ["Saab", "Volvo", "BMW"];

let bmw = cars[2];
console.log(bmw);

cars[0] = "Mercedes";
console.log(cars);

cars.pop();
console.log(cars);

cars.push("Audi");
console.log(cars);

cars.splice(1, 2);
console.log(cars);