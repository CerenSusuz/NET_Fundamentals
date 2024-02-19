const sentence = "I can eat bananas all day";
let word = sentence.slice(10, 17);
console.log(word.toUpperCase());

let cars = ["Saab", "Volvo", "BMW"];

const bmw = cars.find(car => car === "BMW");
console.log(bmw);

cars = ["Mercedes", ...cars.slice(1)];
console.log(cars);

let newCars = [...cars];
newCars.pop();
console.log(newCars);

newCars = [...newCars, "Audi"];
console.log(newCars);

newCars = newCars.filter(car => car !== "Volvo" && car !== "Audi") ;
console.log(newCars);

const cars2 = ["Saab", "Volvo", "BMW"];

const bmw2 = cars2.find(car => car === "BMW");
console.log(bmw2);