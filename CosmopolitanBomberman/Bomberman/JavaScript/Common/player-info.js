function fillPlayerInfo() {
    const username = localStorage.getItem('username');

    const usernameHeader = document.getElementsByTagName('h1')[0];
    usernameHeader.innerHTML = `User: ${localStorage.getItem('username')}`;
}