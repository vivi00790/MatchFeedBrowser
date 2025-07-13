import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import './MatchBrowser.css';

const MatchBrowser = () => {
    const [scheduled, setScheduled] = useState([]);
    const [live, setLive] = useState([]);
    const [ended, setEnded] = useState([]);
    const [tab, setTab] = useState('scheduled');

    useEffect(() => {
        const fetchData = async () => {
            const baseUrl = 'http://localhost:5105/api/match';

            try {
                const [scheduledRes, liveRes, endedRes] = await Promise.all([
                    fetch(`${baseUrl}/scheduled`),
                    fetch(`${baseUrl}/live`),
                    fetch(`${baseUrl}/ended`)
                ]);

                setScheduled(await scheduledRes.json());
                setLive(await liveRes.json());
                setEnded(await endedRes.json());
            } catch (err) {
                console.error('Failed to fetch match data:', err);
            }
        };
        fetchData();
    }, []);

    const renderMatchCard = (match) => (
        <motion.div
            key={match.id}
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: -10 }}
            transition={{ duration: 0.3 }}
            className="match-card"
        >
            <h3>{match.teams[0]} vs {match.teams[1]}</h3>
            <p className="game">{match.game}</p>
            <p><strong>Tournament:</strong> {match.tournament} – {match.stage}</p>
            <p>Status: <span className={`status ${match.status}`}>{match.status}</span></p>
            <p>Start: {new Date(match.startTime).toLocaleString()}</p>
            {match.status === 'live' && (
                <>
                    <p><strong>Current Map:</strong> {match.currentMap}</p>
                    <p>Score: {match.teams[0]} {match.score[match.teams[0]]} - {match.score[match.teams[1]]} {match.teams[1]}</p>
                </>
            )}
            {match.status === 'ended' && (
                <>
                    <p>Final: {match.teams[0]} {match.score[match.teams[0]]} - {match.score[match.teams[1]]} {match.teams[1]}</p>
                    <p>Winner: 🏆 {match.winner}</p>
                </>
            )}
            <a href={match.streamUrl} target="_blank" rel="noreferrer" className="stream-link">Watch Stream</a>
        </motion.div>
    );

    const getMatches = () => {
        switch (tab) {
            case 'scheduled': return scheduled;
            case 'live': return live;
            case 'ended': return ended;
            default: return [];
        }
    };

    return (
        <div className="container">
            <h1 className="title">E-Sports Match Browser</h1>
            <div className="tab-buttons">
                {['scheduled', 'live', 'ended'].map((type) => (
                    <button key={type} onClick={() => setTab(type)} className={tab === type ? 'active' : ''}>
                        {type.charAt(0).toUpperCase() + type.slice(1)}
                    </button>
                ))}
            </div>
            <div className="match-list">
                <AnimatePresence>
                    {getMatches().map(renderMatchCard)}
                </AnimatePresence>
            </div>
        </div>
    );
};

export default MatchBrowser;