const MusicPlayer = {
    queue: [],
    currentIndex: -1,
    currentHowl: null,

    saveState: function () {
        localStorage.setItem('zekterfyQueue', JSON.stringify(this.queue));
        localStorage.setItem('zekterfyIndex', this.currentIndex);
    },

    loadState: function () {
        const savedQueue = localStorage.getItem('zekterfyQueue');
        const savedIndex = localStorage.getItem('zekterfyIndex');
        if (savedQueue && savedIndex) {
            this.queue = JSON.parse(savedQueue);
            this.currentIndex = parseInt(savedIndex);
            if (this.currentIndex >= 0 && this.currentIndex < this.queue.length) {
                const song = this.queue[this.currentIndex];
                document.getElementById("now-playing-title").innerText = song.title;
                this.initHowl(song);
            }
        }
    },

    initHowl: function (song) {
        if (this.currentHowl) this.currentHowl.unload();

        this.currentHowl = new Howl({
            src: [song.url],
            html5: true,
            format: ['mp3'],
            onplay: () => {
                this.logPlayToServer(song.id);
            },
            onend: () => this.playNext()
        });
    },

    addAndPlay: function (songId, title) {
        const songObj = { id: songId, title: title, url: `/Songs/Stream?id=${songId}` };
        this.queue.push(songObj);
        this.currentIndex = this.queue.length - 1;
        this.saveState();

        document.getElementById("now-playing-title").innerText = title;
        this.initHowl(songObj);
        this.currentHowl.play();
    },

    playNext: function () {
        if (this.currentIndex >= this.queue.length - 1) {
            document.getElementById("now-playing-title").innerText = "Черга закінчилась";
            return;
        }
        this.currentIndex++;
        this.saveState();
        const nextSong = this.queue[this.currentIndex];
        document.getElementById("now-playing-title").innerText = nextSong.title;

        this.initHowl(nextSong);
        this.currentHowl.play();
    },

    logPlayToServer: function (songId) {
        console.log("Відправка в історію для пісні ID: " + songId);
        fetch(`/Histories/LogPlay?songId=${songId}`, {
            method: 'POST'
        });
    },

    pause: function () { if (this.currentHowl) this.currentHowl.pause(); },
    resume: function () { if (this.currentHowl) this.currentHowl.play(); },

    addToQueue: function (songId, title) {
        const songObj = { id: songId, title: title, url: `/Songs/Stream?id=${songId}` };
        this.queue.push(songObj);
        this.saveState();
        alert(`Пісню "${title}" додано в чергу!`);
    }
};

document.addEventListener("DOMContentLoaded", function () {
    MusicPlayer.loadState();
});