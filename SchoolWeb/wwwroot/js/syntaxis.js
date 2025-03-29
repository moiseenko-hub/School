// var name = "Lera";

let age = 20;
age = 21;

const maxAge = 100;
maxAge = 1;

let test = 1;
test = "qwe";
test = "qwe";
test = true;
const user = {
  name: "Ivan",
};

console.log(user.name);

user.age = 30;

console.log(user.age);

function SayHi() {
  console.log("hi");
}
let SayBye = function () {
  console.log("hi");
};

let SayFun = () => {
  console.log("fun");
};

SayHi();

user.Speak = SayHi;
user.Speak(); // hi

user.Speak = SayBye;
user.Speak();

let magic; // undefined
if (null == undefined) {
  // true
}
let magicEmpty = {};

let isAdult = false;
const condition1 = isAdult || true;
const condition2 = (condition1 && isAdult && false) || true;

if (user.age) {
  console.log("+");
} else {
  console.log("-");
}

const users = [];
const ages = [1, 5, 2, 50];

for (let i = 0; i < ages.length; i++) {
  const a = ages[i];
  console.log(a);
}
