

function formatUTCDateTimeForDisplay(utcDateTime) {
  var utcDT = new Date(utcDateTime);
  return utcDT.toLocaleDateString() + "  " + utcDT.toLocaleTimeString();
}

// well... we don't need a WebWorker here. setTimeout/Interval is doing the job so far...
//if (typeof (ww) == "undefined") {
//  ww = new Worker("backgroundWorker.js");
//  ww.onmessage = function (event) {
//    alert('got pinged');
//  }
//}

function setUpdatedHeader() {
  $('#updateTimeHeader').text('last update: ' + (new Date()).toLocaleTimeString());
}

var refreshResultsTable = function () {
  // $("#summaryTable").empty();
  $(".dataRow").remove();
  // addHeaderRowToTable('summaryTable');
  $.ajax({
    url: 'http://localhost/Monitoring/Api/MostRecentEventDetails_vw', success: function (result) {
      var listOfLogEntries = result;
      jQuery.each(listOfLogEntries, function (i, entry) {
        addDataRowToTable('summaryTable', entry);
      });
    }
  });
  setUpdatedHeader();
}

setInterval(refreshResultsTable, 60000);
setUpdatedHeader();

var addHeaderRowToTable = function (tableId) {
  var firstRow = document.createElement('tr');
  var testIdCell = document.createElement('td');
  testIdCell.append(document.createTextNode('Test ID'));
  testIdCell.setAttribute('class', 'smallInt');

  var successCell = document.createElement('td');
  successCell.append(document.createTextNode('Successful?'));

  var hostCell = document.createElement('td');
  hostCell.append(document.createTextNode('Hostname'));

  var shortDescCell = document.createElement('td');
  shortDescCell.append(document.createTextNode('Description'));

  var timeStampCell = document.createElement('td');
  timeStampCell.append(document.createTextNode('Timestamp'));
  timeStampCell.setAttribute('class', 'smallDateTime');

  var messageCell = document.createElement('td');
  messageCell.append(document.createTextNode('Message'));

  firstRow.append(testIdCell);
  firstRow.append(successCell);
  firstRow.append(hostCell);
  firstRow.append(shortDescCell);
  firstRow.append(timeStampCell);
  firstRow.append(messageCell);
  $("#" + tableId).append(firstRow);
}

var addDataRowToTable = function (tableId, rowEntry) {

  var nextRow = document.createElement('tr');
  nextRow.setAttribute('class', 'dataRow');

  var testIdCell = document.createElement('td');
  testIdCell.append(document.createTextNode(rowEntry.TestID));

  var successCell = document.createElement('td');
  successCell.append(document.createTextNode(rowEntry.Successful));
  if (!rowEntry.Successful) {
    nextRow.setAttribute('class', 'dataRow alert');
  }

  var hostCell = document.createElement('td');
  hostCell.append(document.createTextNode(rowEntry.TargetMachine));

  var shortDescCell = document.createElement('td');
  shortDescCell.append(document.createTextNode(rowEntry.TestDisplayName));

  var timeStampCell = document.createElement('td');
  if (rowEntry.Successful && (((new Date()) - (new Date(rowEntry.TimeStamp))) > 300000)) {
    nextRow.setAttribute('class', 'dataRow warning');
    timeStampCell.setAttribute('title', 'outdated?');
  }
  timeStampCell.append(document.createTextNode(formatUTCDateTimeForDisplay(rowEntry.TimeStamp)));

  var messageCell = document.createElement('td');
  messageCell.append(document.createTextNode(rowEntry.FeedbackMessage));

  nextRow.append(testIdCell);
  nextRow.append(successCell);
  nextRow.append(hostCell);
  nextRow.append(shortDescCell);
  nextRow.append(timeStampCell);
  nextRow.append(messageCell);

  $("#" + tableId).append(nextRow);
}


$.ajax({
  url: 'http://localhost/Monitoring/Api/MostRecentEventDetails_vw', success: function(result){
		var listOfLogEntries = result;
		jQuery.each( listOfLogEntries, function( i, entry ) {
      addDataRowToTable('summaryTable', entry);
		});
		
  }});


