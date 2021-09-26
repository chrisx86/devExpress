const daysEl = document.getElementById('days');

const newYears = '1 Jan 2021';

function countdown(){
  const newYearsDate = new Date(newYears);
  const currentDate = new Date();

  const totalSeconds = (newYears - currentDate) / 1000;
  console.log(newYears - currentDate);

  const days = Math.floor(totalSeconds/3600/24);
  const hours = Math.floor(totalSeconds/3600) % 24;
  const minutes = Math.floor(totalSeconds/60) % 60;
  const seconds = Math.floor(totalSeconds) % 60;
  
  daysEl.innerHTML = days;

}

function formatTime(time){
  return time < 10 ? `0${time} `: time;
}


countdown();

setInterval(countdown, 1000);