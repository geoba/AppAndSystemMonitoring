var pleaseGoOnForever = true;

function postToPage() {
  postMessage(1);
}

function callingMyselfRecursively() {
  if (pleaseGoOnForever) {
    postToPage();
    setTimeout(callingMyselfRecursively(), 6000);
  }
}

callingMyselfRecursively();

