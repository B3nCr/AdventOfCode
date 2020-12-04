import fs from "fs";

var array = fs.readFileSync("./2/file.txt").toString().split("\r\n");

const valid = (string) => {
  var parts = string.split(" ");
  var minMax = parts[0].split("-");
  var char = parts[1][0];
  var password = parts[2];
  console.log(`${minMax[0]}-${minMax[1]} ${char} ${password}`);
  return positionMatch(char, password, minMax);
};

var count = 0;

array.forEach((element) => {
  if (valid(element)) {
    count++;
  }
});

console.log(count);
function positionMatch(char, password, minMax) {
  return (password[minMax[0]-1] == char || password[minMax[1]-1] == char) && !(password[minMax[0]-1] == char && password[minMax[1]-1] == char)
}

function regexMatch(char, password, minMax) {
  var regex = new RegExp(char, "g");
  var matches = password.match(regex);
  return (
    matches != null &&
    matches.length >= minMax[0] &&
    matches.length <= minMax[1]
  );
}
