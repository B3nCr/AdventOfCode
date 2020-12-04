import fs from "fs";

var array = fs.readFileSync("./1/file.txt").toString().split("\r\n");

array.sort((a, b) => a - b);

var max = array.length;
var result = -1;

for (var lowIndex = 0; lowIndex < array.length; lowIndex++) {
  for (var mid = 1; mid < array.length; mid++) {
    for (var rightHand = 2; rightHand < array.length; rightHand++) {
      var sum =
        parseInt(array[lowIndex]) +
        parseInt(array[mid]) +
        parseInt(array[rightHand]) +
        0;

      console.info(
        `wft ${array[lowIndex]} ${array[mid]} ${array[rightHand]} = ${sum}`
      );

      if (sum == 2020) {
        result = array[lowIndex] * array[mid] * array[rightHand];
        console.error(result);
        break;
      } else if (sum > 2020) {
        console.warn(`Maxed out ${lowIndex}`);
        lowIndex++;
        mid = lowIndex + 1;
        rightHand = mid + 1;
      } else if (result != -1) {
        lowIndex = array.length;
        mid=array.length;
        rightHand = array.length;
      }
    }
  }
}
