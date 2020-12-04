import { group, profileEnd } from "console";
import fs from "fs";

var people = fs.readFileSync("./4/file.txt").toString().split("\r\n\r\n");

console.log(people.length);

var count = 0;
people.forEach((x) => {
  if (validatePassport(x)) {
    count++;
  }
});

console.log(count);

function validatePassport(personstr) {
  var properties = personstr.replace(/\r\n/g, " ").split(" ");

  var person = {};

  properties.forEach((prop) => {
    var keyValue = prop.split(":");
    person[keyValue[0]] = keyValue[1];
  });

  var res = validatePerson(person);

  return res;
}

function validatePerson(person) {
  console.log(JSON.stringify(person));
  if (!validateRequiredKeys(Object.keys(person))) {
    return false;
  }
  if (!validateYear(person.byr, 1920, 2002)) {
    console.log("Invalid byr: ", person.byr);
    return false;
  }
  if (!validateYear(person.iyr, 2010, 2020)) {
    console.log("Invalid iyr: ", person.iyr);
    return false;
  }
  if (!validateYear(person.eyr, 2020, 2030)) {
    console.log("Invalid eyr: ", person.eyr);
    return false;
  }
  if (!validateHeight(person.hgt)) {
    console.log("Invalid hgt: ", person.hgt);
    return false;
  }

  if (!validateHairColour(person.hcl)) {
    console.log("Invalid hcl: ", person.hcl);
    return false;
  }

  if (!validateEyeColour(person.ecl)) {
    console.log("Invalid ecl: ", person.ecl);
    return false;
  }

  if (!validatePassportId(person.pid)) {
    console.log("Invalid pid: ", person.pid);
    return false;
  }

  return true;
}

function validateRequiredKeys(keys) {
  const required = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"];
  required.sort();
  var result = true;
  required.forEach((key) => {
    if (!keys.includes(key)) {
      result = false;
    }
  });
  return result;
}

function validateYear(str, min, max) {
  var year = parseInt(str);
  if (year == null) return false;
  return year >= min && year <= max;
}

function validateHeight(height) {
  var groups = height.match(/(\d+)(cm|in)/);
  console.log(groups);

  if (groups == null) {
    return false;
  }

  var height = parseInt(groups[1]);
  if (groups[2] == "in") {
    return height >= 59 && height <= 76;
  } else {
    return height >= 150 && height <= 193;
  }
}

function validateHairColour(hair) {
  return hair.match(/#[a-f0-9]{6}/);
}

function validateEyeColour(eyes) {
  const validColours = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];
  return validColours.includes(eyes);
}

function validatePassportId(pid){
    return pid.length == 9 && pid.match(/\d{9}/)
}