import React, { useState, useEffect } from 'react';

const MatchBrowser = () => {
    const [scheduled, setScheduled] = useState([]);
    const [live, setLive] = useState([]);
    const [ended, setEnded] = useState([]);
    const [tab, setTab] = useState('scheduled');

    useEffect(() => {
        const fetchData = async () => {
            const [scheduledRes, liveRes, endedRes] = await Promise.all([
                fetch('/data/matches-scheduled.json'),
                fetch('/data/matches-live.json'),
                fetch('/data/matches-ended.json'),
            ]);
            setScheduled(await scheduledRes.json());
            setLive(await liveRes.json());
            setEnded(await endedRes.json());
        };
        fetchData();
    }, []);

    const renderMatchCard = (match) => {
        return (
            <div key={match.id} style={{ border: '1px solid #ccc', marginBottom: '10px', padding: '10px', borderRadius: '8px' }}>
                <h3>{match.teams[0]} vs {match.teams[1]}</h3>
                <p><strong>Game:</strong> {match.game}</p>
                <p><strong>Tournament:</strong> {match.tournament} – {match.stage}</p>
                <p><strong>Status:</strong> {match.status}</p>
                <p><strong>Start Time:</strong> {new Date(match.startTime).toLocaleString()}</p>
                {match.status === 'live' && (
                    <>
                        <p><strong>Current Map:</strong> {match.currentMap}</p>
                        <p><strong>Score:</strong> {match.teams[0]} {match.score[match.teams[0]]} - {match.score[match.teams[1]]} {match.teams[1]}</p>
                    </>
                )}
                {match.status === 'ended' && (
                    <>
                        <p><strong>Final Score:</strong> {match.teams[0]} {match.score[match.teams[0]]} - {match.score[match.teams[1]]} {match.teams[1]}</p>
                        <p><strong>Winner:</strong> {match.winner}</p>
                    </>
                )}
                <p><a href={match.streamUrl} target="_blank" rel="noreferrer">Watch Stream</a></p>
            </div>
        );
    };

    const getMatchesForTab = () => {
        switch (tab) {
            case 'scheduled': return scheduled;
            case 'live': return live;
            case 'ended': return ended;
            default: return [];
        }
    };

    return (
        <div style={{ maxWidth: '700px', margin: 'auto', padding: '20px' }}>
            <h1>E-Sports Match Browser</h1>
            <div style={{ marginBottom: '20px' }}>
                {['scheduled', 'live', 'ended'].map((type) => (
                    <button key={type} onClick={() => setTab(type)} style={{ marginRight: '10px', fontWeight: tab === type ? 'bold' : 'normal' }}>
                        {type.charAt(0).toUpperCase() + type.slice(1)}
                    </button>
                ))}
            </div>
            <div>
                {getMatchesForTab().map(renderMatchCard)}
            </div>
        </div>
    );
};

export default MatchBrowser;