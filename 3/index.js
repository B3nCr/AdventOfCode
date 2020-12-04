import fs from "fs";

var array = fs.readFileSync("./3/file.txt").toString().split("\r\n");

const length = array[0].length;

var count = CountTrees(1, 1) *
  CountTrees(3, 1) *
  CountTrees(5, 1) *
  CountTrees(7, 1) *
  CountTrees(1, 2);

console.log(count);

function CountTrees(right, down) {
  var x = 0;
  var count = 0;

  for (var y = 0; y < array.length; y += down) {
    var element = array[y];
    if (element[x] === "#") {
      count++;
    }
    console.log(`${element[x]} ${x},${y} ${count}`);

    x += right;
    if (x >= length) {
      x = x - length;
    }
  }
  return count;
}
