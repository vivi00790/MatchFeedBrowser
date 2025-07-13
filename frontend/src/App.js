import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import MatchBrowser from './pages/MatchBrowser';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<MatchBrowser />} />
                {/* Add more routes here if needed */}
            </Routes>
        </Router>
    );
}

export default App;