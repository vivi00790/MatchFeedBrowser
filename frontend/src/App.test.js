import { render, screen, fireEvent } from '@testing-library/react';
import App from './App';

beforeEach(() => {
  global.fetch = jest.fn((url) => {
    const status = url.split('/').pop();
    const mockData = {
      scheduled: [
        {
          id: 1,
          teams: ['Team A', 'Team B'],
          game: 'Valorant',
          tournament: 'VCT',
          stage: 'Group',
          status: 'scheduled',
          startTime: new Date().toISOString(),
          streamUrl: '#',
        },
      ],
      live: [
        {
          id: 2,
          teams: ['Team C', 'Team D'],
          game: 'Valorant',
          tournament: 'VCT',
          stage: 'Group',
          status: 'live',
          currentMap: 'Bind',
          score: { 'Team C': 5, 'Team D': 7 },
          startTime: new Date().toISOString(),
          streamUrl: '#',
        },
      ],
      ended: [
        {
          id: 3,
          teams: ['Team E', 'Team F'],
          game: 'Valorant',
          tournament: 'VCT',
          stage: 'Final',
          status: 'ended',
          score: { 'Team E': 13, 'Team F': 11 },
          winner: 'Team E',
          startTime: new Date().toISOString(),
          streamUrl: '#',
        },
      ],
    };

    return Promise.resolve({
      ok: true,
      json: () => Promise.resolve(mockData[status]),
    });
  });
});

afterEach(() => {
  jest.clearAllMocks();
});

describe('MatchBrowser UI', () => {
  it('renders scheduled matches by default', async () => {
    render(
        <App />
    );

    expect(await screen.findByText(/Team A vs Team B/)).toBeInTheDocument();
  });

  it('switches to live matches when clicking Live tab', async () => {
    render(
        <App />
    );

    fireEvent.click(screen.getByText('Live'));

    expect(await screen.findByText(/Team C vs Team D/)).toBeInTheDocument();
    expect(await screen.findByText(/Current Map:/)).toBeInTheDocument();
  });

  it('switches to ended matches and shows winner', async () => {
    render(
        <App />
    );

    fireEvent.click(screen.getByText('Ended'));

    expect(await screen.findByText(/Team E vs Team F/)).toBeInTheDocument();
    expect(await screen.findByText(/Winner:/)).toBeInTheDocument();
  });
});
